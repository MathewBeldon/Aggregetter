package crawler

import (
	"hash/fnv"
	"io"
)

func RequestHash(url string, body io.Reader) uint64 {
	h := fnv.New64a()
	h.Write([]byte(url))
	if body != nil {
		io.Copy(h, body)
	}
	return h.Sum64()
}
