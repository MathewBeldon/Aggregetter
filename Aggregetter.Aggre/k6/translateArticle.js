import http from 'k6/http';
import { check } from 'k6';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.1.0/index.js';

export const options = {    
    vus: 100,
    duration: '600s',
};

export default function () {
    let data = { articleSlug: 'lorem-ipsum' + randomIntBetween(1, 200) };
    let url = 'https://localhost:5001/api/v1/articles/translate';
    let res = http.post(url, JSON.stringify(data), {
        headers: { 'Content-Type': 'application/json', 'accept': '*/*' },
      });
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
};

