import * as api from '$lib/utils/api';
import { setCookie } from './setCookie';

export async function post(request) {
	// TODO individual properties
	const body = await api.post('account/register', request.body, null);
	return setCookie(body);
}
