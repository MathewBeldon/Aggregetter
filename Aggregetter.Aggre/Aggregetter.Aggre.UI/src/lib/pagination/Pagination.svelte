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
		for (let i = currentPage + 1; i < pageCount && i < (currentPage + 3); ++i) {
			next.push(i);
		}
	}	
</script>

<p>{pageCount}</p>
<p>{currentPage}</p>

{#if pageCount > 1}
	<nav>
		<ul class="pagination">
			{#each previous as n}
				<li class="page-item" class:active={n == currentPage}><a class="page-link" href={href(n)}>{n}</a></li>
			{/each}

			{#each next as n}
				<li class="page-item" class:active={n == currentPage}><a class="page-link" href={href(n)}>{n}</a></li>
			{/each}
		</ul>
	</nav>
{/if}
