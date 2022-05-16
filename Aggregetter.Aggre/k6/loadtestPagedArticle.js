import http from 'k6/http';
import { check } from 'k6';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.1.0/index.js';

export const options = {
    scenarios: {
        constant_request_rate: {
              executor: 'constant-arrival-rate',
              rate: 10000,
              timeUnit: '1s',
              duration: '30s',
              preAllocatedVUs: 1000,
              maxVUs: 2000, 
        },
    },
};

export default function () {
    const res = http.get('https://localhost:61301/api/v1/articles?PageSize=20&Page=' + randomIntBetween(1, 10));
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
};