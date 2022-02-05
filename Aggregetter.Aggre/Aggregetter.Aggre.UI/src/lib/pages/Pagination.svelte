<script lang="ts">
	export let pageCount;
	export let currentPage;
	export let href;
	let previous;
	
	$: {
		previous = [];
		for (let i = currentPage - 1; i > 0 && i > (currentPage - 3); --i) {
			previous.push(i);
		}
	}
	let next;
	$: {
		next = [];
		for (let i = currentPage + 1; i <= pageCount && i < (currentPage + 3); ++i) {
			next.push(i);
		}
	}	
</script>

{#if pageCount > 1}
	<div class="pagination">
		<a href="{href(1)}">&laquo;</a>
		{#each previous.reverse() as n}
			<a href={href(n)}>{n}</a>
		{/each}
			<a class="active">{currentPage}</a>
		{#each next as n}
			<a href={href(n)}>{n}</a>
		{/each}
		<a href="{href(pageCount)}">&raquo;</a>
	</div>
{/if}

<style>
	.pagination {
		display: inline-block;
		margin: 20px auto;
		width: 100%;
	}

	.pagination a {
		color: var(--primary-color);
		float: left;
		padding: 8px 16px;
		text-decoration: none;
		transition: background-color .3s;
	}

	.pagination a.active {
		background-color: var(--primary-color);
		color: var(--off-white);
	}

	.pagination a:hover {
		background-color:  var(--tertiary-color);
		color: var(--off-white-light);
	}

	.pagination a.active:hover {
		background-color: var(--primary-color);
		color: var(--off-white);
	}
</style>

