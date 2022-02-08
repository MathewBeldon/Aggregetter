<script>
	export let article;
	//export let user;
</script>
<div class="article-preview">
	<a href="/article/{article.articleSlug}" class="title"> {article.translatedTitle} </a>
	<div class="info-container"> 
		<span class="sub-title"> Category: </span> 
		<a class="sub-title-link" href="/">{article.category.name} </a> 
		<span class="sub-title"> Provider: </span> 
		<a class="sub-title-link" href="/"> {article.provider.name} </a> 
		<a href="{article.provider.baseAddress}{article.endpoint}"><svg class="icon-link"><use xlink:href="/svg/icons.svg#gg-external-link"/></svg></a>
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
		flex-flow:row wrap;
	}
	.article-preview > *{
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
		flex-flow: nowrap;
		background-color: yellow;
	}
	.icon-link {
		color: var(--secondary-color);
		width: 25px;
		height: 25px;
		margin-top: 5px;
		margin-bottom: -4px;

	}
	.sub-title {
		color: var(--tertiary-color);
		flex-grow: 1;
		background-color: pink;
	}
	.sub-title-link {
		color: var(--primary-color);
		flex-grow: 2;		
	}
	.title {
		color: var(--primary-color);
		font-size: 1.5em;
		font-weight: 400;
		margin: 5px;
	}

</style>