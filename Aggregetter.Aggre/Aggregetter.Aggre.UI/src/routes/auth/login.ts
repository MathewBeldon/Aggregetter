import * as api from '$lib/utils/api';
import { setCookie } from './setCookie';

export async function post(request) {
	const body = await api.post('accounts/authenticate', {
			email: request.body.email,
			password: request.body.password
			
		}, null);
	return setCookie(body);
}
