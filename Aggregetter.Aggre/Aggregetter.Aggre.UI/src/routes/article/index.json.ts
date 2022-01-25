import * as api from '$lib/utils/api';
import { page_size } from '$lib/utils/constants';

export async function get({ query, locals }) {
	const page = query.get('page') || 1;
	const articles = await api.get(
		`article?page=${page}&pageSize=${page_size}`,
		locals.user && locals.user.token
	);
	
	return {
		body: {
			articles
		}
	};
}
