import http from 'k6/http';
import { check } from 'k6';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.1.0/index.js';

export const options = {    
    vus: 100,
    duration: '600s',
};

export default function () {
    const res = http.get('http://localhost:5000/api/v1/articles?PageSize=20&Page=' + randomIntBetween(1, 10));
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
};