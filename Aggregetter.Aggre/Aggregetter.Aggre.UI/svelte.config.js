import auto from '@sveltejs/adapter-auto';
import sveltePreprocess from 'svelte-preprocess';

export default {
	preprocess: sveltePreprocess(),
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