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
	export let translated = false;
	export let translatedText = "Translate"

	function toggle() {
		translated = !translated;
		if (translated){
			translatedText = "Original"
		} else {
			translatedText = "Translate"
		}
	}
</script>

<svelte:head>
	<title>{article.translatedTitle}</title>
</svelte:head>

<button  on:click={toggle}>{translatedText}</button>

<div>
	{#if translated}
		<h1>{article.translatedTitle}</h1>
		<p>{article.translatedBody}</p>
	{/if}

	{#if !translated}
		<h1>{article.originalTitle}</h1>
		<p>{article.originalBody}</p>
	{/if}
</div>


<style>
	h1 {
		color: var(--primary-color);
	}

	div {
		background-color: var(--off-white);
		margin: 20px 0 20px 0;
		padding: 10px;		
		border: solid 1px var(--secondary-color);
		border-bottom: solid 2px var(--primary-color);
	}
</style>