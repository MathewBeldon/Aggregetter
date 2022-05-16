import http from 'k6/http';
import { check } from 'k6';
import { randomIntBetween } from 'https://jslib.k6.io/k6-utils/1.1.0/index.js';

export const options = {
    scenarios: {
        constant_request_rate: {
              executor: 'constant-arrival-rate',
              rate: 10000,
              timeUnit: '1s', // 1000 iterations per second, i.e. 1000 RPS
              duration: '30s',
              preAllocatedVUs: 1000, // how large the initial pool of VUs would be
              maxVUs: 2000, // if the preAllocatedVUs are not enough, we can initialize more
        },
    },
};

export default function () {
    const res = http.get('https://localhost:61301/api/v1/articles/lorem-ipsum' + randomIntBetween(1, 200));
    check(res, {
        'is status 200': (r) => r.status === 200,
    });
};