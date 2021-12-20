<script>
	export let article;
	export let user;
	console.log(article);
	async function toggle_favorite() {
		// optimistic UI
		// if (article.favorited) {
		// 	article.favoritesCount -= 1;
		// 	article.favorited = false;
		// } else {
		// 	article.favoritesCount += 1;
		// 	article.favorited = true;
		// }

		// ({ article } = await (article.favorited
		// 	? api.post(`articles/${article.slug}/favorite`, null, user && user.token)
		// 	: api.del(`articles/${article.slug}/favorite`, user && user.token)));
	}
</script>

<div class="article-preview">
	<div class="article-meta">
		<a href="/article/{article.articleSlug}">
			<p> {article.translatedTitle} </p>
		</a>
<!-- 
		<div class="info">
			<a class="author" href="/profile/@{article.author.username}"> {article.author.username} </a>
			<span class="date"> {new Date(article.createdAt).toDateString()} </span>
		</div>

		{#if user}
			<div class="pull-xs-right">
				<button
					class="btn btn-sm {article.favorited ? 'btn-primary' : 'btn-outline-primary'}"
					on:click={toggle_favorite}>

					<i class="ion-heart" />
					{article.favoritesCount}
				</button>
			</div>
		{/if}
	

	<a href="/article/{article.slug}" rel="prefetch" class="preview-link">
		<h1>{article.title}</h1>
		<p>{article.description}</p>
		<span>Read more...</span>
		<ul class="tag-list">
			{#each article.tagList as tag}
				<li class="tag-default tag-pill tag-outline"><a href="/?tag={tag}">{tag}</a></li>
			{/each}
		</ul>
	</a> -->
	</div>
</div>
