<script>
	export let article;
	export let user;
</script>

<div class="article-preview">
	<div class="article-meta">
		<a href="/article/{article.articleSlug}">
			<p> {article.translatedTitle} | Category: {article.category.name} | Provider: {article.provider.name}  </p>
			<a href="https://{article.provider.baseAddress}/{article.endpoint}">{article.provider.baseAddress}/{article.endpoint}</a>
		</a>
		<!-- need to improve external link-->
	</div>
</div>
