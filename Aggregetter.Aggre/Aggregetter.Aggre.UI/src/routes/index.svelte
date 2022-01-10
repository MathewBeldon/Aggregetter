<script context="module">
	export async function load({ page, fetch }) {
		const [{ articles, pages }] = await Promise.all([
			fetch(`/article.json?${page.query}`, { credentials: 'include' }).then((r) => r.json())
		]);

		return {
			props: {
				articles,
				pages,
			}
		};
	}
</script> 

<script>
	import { page, session } from '$app/stores';
	import ArticleList from '$lib/article/ArticleList.svelte';
	import Pagination from '$lib/Pagination/Pagination.svelte';


	export let articles;
	export let pages;


	$: p = $page.query.get('p') || 1;
</script>

<svelte:head>
	<title>Aggregetter</title>
</svelte:head>

<div class="home-page">
	{#if !$session.user}
		<div class="banner">
			<div class="container">
				<h1 class="logo-font">Aggregetter</h1>
			</div>
		</div>
	{/if}

	<div class="container page">
		<div class="row">
			<div class="col-md-9">
				<ArticleList {articles} />
				<Pagination {pages} {p} href={(p) => `?page=${p}`} />
			</div>

			
		</div>
	</div>
</div>
