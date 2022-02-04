<script context="module" lang="ts">
	export async function load({ page, fetch }) {
		const { slug } = page.params;
		const [ article ] = await Promise.all([
			fetch(`/article/${slug}.json`).then((r) => r.json())
		]);
        
		return {
			props: { article, slug }
		};
	}
</script>

<script lang="ts">
	import { session } from '$app/stores';

	export let article;
	export let slug: string;
</script>

<svelte:head>
	<title>{article.translatedTitle}</title>
</svelte:head>

<div>
    <h1>{article.translatedTitle}</h1>
    <p>{article.translatedBody}</p>

    <h1>{article.originalTitle}</h1>
    <p>{article.originalBody}</p>
</div>
