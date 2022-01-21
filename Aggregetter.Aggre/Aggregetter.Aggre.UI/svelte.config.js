import auto from '@sveltejs/adapter-auto';

export default {
	kit: {
		adapter: auto(),
		target: '#svelte',
		vite: {
            define: {
                'process.env': process.env,
            },
        },
	}
};