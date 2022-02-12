<script>
	export let article;
	//export let user;
</script>
<div class="article-preview">
	<a href="/article/{article.articleSlug}" class="title"> {article.translatedTitle} </a>
	<div class="info-container">
		<div class="info-sub">
			<a class="sub-title-provider" href="/">{article.provider.name}</a>
			<div class="info-categories">
				<a class="sub-title-category" href="/">{article.category.name}</a>
				<a class="sub-title-category" href="/">{article.category.name}</a>
				<a class="sub-title-category" href="/">{article.category.name}</a>
				<a class="sub-title-category" href="/">{article.category.name}</a>
				<a class="sub-title-category" href="/">{article.category.name}</a>

			</div>
		</div>
		<div class="info-link">
			<a href="{article.provider.baseAddress}{article.endpoint}"><svg class="icon-link"><use xlink:href="/svg/icons.svg#gg-external-link"/></svg></a>
			
		</div>	
	</div>	
</div>

<style>

	.article-preview {
		background-color: var(--off-white);
		margin-bottom: 1rem;
		padding: 5px;
		border: solid 1px var(--secondary-color);
		border-bottom: solid 2px var(--primary-color);
		display: flex;
	}
	.article-preview > * {
		flex: 1 100%;
	}

	.article-preview p {
		padding: 0px;
		margin: 5px;
	}

	.article-preview a {
		text-decoration: none;
	}
	.info-container {
		display: flex;
		flex: 1 100%;
		flex-flow: column;
		padding-left: 5px;
		border-left: 2px solid var(--primary-color);
	}
	.info-categories {
		align-items: flex-start;
	}
	.info-sub {
		display: flex;
		flex: 1 100%;
		flex-flow: column;
		gap:10px;
	}
	.icon-link {
		color: var(--secondary-color);
		width: 25px;
		height: 25px;
		margin-top: 5px;
		margin-bottom: -4px;
	}
	.sub-title-provider {		
		color: var(--primary-color);	
		font-size: 1.2em;
		margin-top: 4px;
		margin-left: 2px;
	}
	.sub-title-category {
		color: var(--off-white);
		background-color: var(--primary-color);
		padding: 0 4px 0 4px;
		border-radius: 4px;
		display: inline-block;
		margin-bottom: 5px;
	}
	.title {
		color: var(--primary-color);
		font-size: 1.2em;
		margin: 5px;
	}

</style>