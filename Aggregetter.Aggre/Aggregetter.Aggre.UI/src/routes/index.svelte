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


	$: p = +$page.query.get('p') || 1;
</script>

<svelte:head>
	<title>Conduit</title>
</svelte:head>

<div class="home-page">
	{#if !$session.user}
		<div class="banner">
			<div class="container">
				<h1 class="logo-font">conduit</h1>
				<p>A place to share your knowledge.</p>
			</div>
		</div>
	{/if}

	<div class="container page">
		<div class="row">
			<div class="col-md-9">
				<div class="feed-toggle">
					<ul class="nav nav-pills outline-active">

						{#if $session.user}
							<li class="nav-item">
								<a href="/?tab=feed" rel="prefetch" class="nav-link">
									Your Feed
								</a>
							</li>
						{:else}
							<li class="nav-item">
								<a href="/login" rel="prefetch" class="nav-link">Sign in to see your Feed </a>
							</li>
						{/if}
					</ul>
				</div>
				<ArticleList {articles} />
				<Pagination {pages} {p} href={(p) => `/?&page=${p}`} />
			</div>

			
		</div>
	</div>
</div>
