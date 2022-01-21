<script>
	export let article;
	export let user;
</script>

<div class="article-preview">
	<a href="/article/{article.articleSlug}">
		<p> {article.translatedTitle} | Category: {article.category.name} | Provider: {article.provider.name}  </p>
		<a href="https://{article.provider.baseAddress}/{article.endpoint}">{article.provider.baseAddress}/{article.endpoint}</a>
	</a>
</div>

<style>

	.article-preview {
		background-color: var(--off-white);
		margin: 3px;
		padding: 10px;
		border: solid 1px var(--primary-color);
	}

	.article-preview a > p {
		padding: 0px;
		margin: 0px;
	}

	.article-preview a {
		text-decoration: none;
	}

</style>