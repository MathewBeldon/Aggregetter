<script context="module">
	export async function load({ page, fetch }) {
		const [{ articles }] = await Promise.all([
			fetch(`/article.json?${page.query}`, { credentials: 'include' }).then((r) => r.json())
		]);

		return {
			props: {
				articles
			}
		};
	}
</script> 

<script lang="ts">
	import { page, session } from '$app/stores';
	import ArticleList from '$lib/article/ArticleList.svelte';
	import Pagination from '$lib/pages/Pagination.svelte';

	export let articles;

	const pageCount = articles.pageCount;
	
	$: currentPage = articles.page || 1;
</script>

<svelte:head>
	<title>Aggregetter</title>
</svelte:head>

<div class="home-page">
	<div>
		<ArticleList {articles} />
		<Pagination {pageCount} {currentPage} href={(p) => `?page=${p}`} />
	</div>
</div>
