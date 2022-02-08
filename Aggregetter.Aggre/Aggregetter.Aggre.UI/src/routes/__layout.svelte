<script lang="ts">
	import { navigating } from '$app/stores';
	import Nav from '$lib/nav/Nav.svelte';
	import '../app.css';
</script>

<Nav />

<main>
	<slot />
</main>

<style>
	main {
		flex: 1;
		display: flex;
		flex-direction: column;
		padding: 1rem;
		width: 100%;
		max-width: 1024px;
		margin: 0 auto;
		box-sizing: border-box;
		background-color: green;
	}
</style>
