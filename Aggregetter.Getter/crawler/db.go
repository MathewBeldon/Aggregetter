package crawler

import (
	"context"
	"log"
	"net/url"
	"strconv"
	"time"

	"go.mongodb.org/mongo-driver/mongo/options"

	"go.mongodb.org/mongo-driver/mongo"

	"go.mongodb.org/mongo-driver/x/bsonx"
)

// Storage implements a MongoDB storage backend for colly
type Storage struct {
	Database          string
	URI               string
	VisitedCollection string "colly_visited"
	CookiesCollection string "colly_cookies"
	PagesCollection   string "colly_pages"
	client            *mongo.Client
	db                *mongo.Database
	visited           *mongo.Collection
	cookies           *mongo.Collection
	pages             *mongo.Collection
}

// Init initializes the MongoDB storage
func (s *Storage) Init() error {

	var err error

	if s.client, err = mongo.NewClient(options.Client().ApplyURI(s.URI)); err != nil {

		return err

	}

	if err = s.client.Connect(context.Background()); err != nil {

		return err

	}

	s.db = s.client.Database(s.Database)

	s.visited = s.db.Collection(s.VisitedCollection)

	s.cookies = s.db.Collection(s.CookiesCollection)

	s.pages = s.db.Collection(s.PagesCollection)

	return nil

}

// Visited implements colly/storage.Visited()
func (s *Storage) Visited(requestID uint64) error {

	_, err := s.visited.InsertOne(context.Background(), bsonx.MDoc{
		"requestID": bsonx.String(strconv.FormatUint(requestID, 10)),
		"visited":   bsonx.Boolean(true),
	})

	return err

}

// IsVisited implements colly/storage.IsVisited()
func (s *Storage) IsVisited(requestID uint64) (bool, error) {

	result := bsonx.MDoc{}

	err := s.visited.FindOne(nil, bsonx.MDoc{
		"requestID": bsonx.String(strconv.FormatUint(requestID, 10)),
	}).Decode(&result)
	if err != nil {

		if err == mongo.ErrNoDocuments {

			return false, nil

		}

		log.Println(err)

		return false, err

	}

	return true, nil

}

// Cookies implements colly/storage.Cookies()
func (s *Storage) Cookies(u *url.URL) string {

	result := bsonx.MDoc{}

	if err := s.cookies.FindOne(nil, bsonx.MDoc{
		"host": bsonx.String(u.Host),
	}).Decode(&result); err != nil {

		if err != mongo.ErrNoDocuments {

			log.Println(err)

		}

		return ""

	}

	return result["cookies"].String()

}

// SetCookies implements colly/storage.SetCookies()
func (s *Storage) SetCookies(u *url.URL, cookies string) {

	if _, err := s.cookies.InsertOne(nil, bsonx.MDoc{
		"host":    bsonx.String(u.Host),
		"cookies": bsonx.String(cookies),
	}); err != nil {

		log.Println(err)

	}

}

// SavePage not part of colly
func (s *Storage) SavePage(requestID uint64, u *url.URL, body []byte) {

	if _, err := s.pages.InsertOne(nil, bsonx.MDoc{
		"requestID": bsonx.String(strconv.FormatUint(requestID, 10)),
		"dateTime":  bsonx.String(time.Now().UTC().String()),
		"url":       bsonx.String(u.String()),
		"body":      bsonx.Binary(0, body),
	}); err != nil {

		log.Println(err)

	}

}
