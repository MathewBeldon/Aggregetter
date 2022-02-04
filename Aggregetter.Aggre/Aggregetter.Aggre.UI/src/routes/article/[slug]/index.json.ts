import * as api from '$lib/utils/api';

export async function get({ params, locals }) {
	const { slug } = params;
	const { data } = await api.get(`article/${slug}`, locals.user && locals.user.token);
	return {
		body: data
	};
}

export async function put(request) {
	console.log('put', request);
}
