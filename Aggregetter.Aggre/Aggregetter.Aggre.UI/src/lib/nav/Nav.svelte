<script>
	import { page, session } from '$app/stores';
	import { goto } from '$app/navigation';
	import { onMount } from "svelte";
	import { post } from "$lib/utils/post"
	
	let showMobileMenu = false;

	// Mobile menu click event handler
	const handleMobileIconClick = () => (showMobileMenu = !showMobileMenu);

	// Media match query handler
	const mediaQueryHandler = e => {
		// Reset mobile state
		if (!e.matches) {
			showMobileMenu = false;
		}
	};

	onMount(() => {
		var mql = window.matchMedia('(max-width: 600px)');

		mql.onchange = (e) => {
			if (e.matches) {
				/* the viewport is 600 pixels wide or less */
				console.log('This is a narrow screen — less than 600px wide.')
			} else {
				/* the viewport is more than than 600 pixels wide */
				console.log('This is a wide screen — more than 600px wide.')
			}
		}
  	});

	async function logout() {
		await post(`auth/logout`);
		// this will trigger a redirect, because it
		// causes the `load` function to run again
		$session.user = null;
		goto('/login');
	}

</script>

<nav>
	<div class="inner">
		<div on:click={handleMobileIconClick} class={`mobile-icon${showMobileMenu ? ' active' : ''}`}>
			<div class="middle-line"></div>
		</div>
		<a href="/"><h1><p class="left-logo">Aggre</p><p class="right-logo">getter</p></h1></a>
		<ul class={`navbar-list${showMobileMenu ? ' mobile' : ''}`}>
			{#if $session.user}
				<li class="nav-item">
					<p>
						{$session.user.username}
					</p>
				</li>
				<li class="nav-item">
					<a rel="prefetch" href="/" class="nav-link" on:click={logout}>
						<svg class="icon"><use xlink:href="/svg//icons.svg#gg-log-out"/></svg>Sign out
					</a>
				</li>

				<!-- <li> 
					<a rel="prefetch" href="/editor" class="nav-link" class:active={$page.path === '/editor'}>
						New Post
					</a>
				</li>

				<li class="nav-item">
					<a
						rel="prefetch"
						href="/settings"
						class="nav-link"
						class:active={$page.path === '/settings'}>
						Settings
					</a>
				</li>

				<li class="nav-item">
					<a rel="prefetch" href="/profile/@{$session.user.username}" class="nav-link">
						
						{$session.user.username}
					</a>
				</li> -->
			{:else}
				<li class="nav-item">
					<a rel="prefetch" href="/login" class:active={$page.path === '/login'}>
						<svg class="icon"><use xlink:href="/svg/icons.svg#gg-log-in"/></svg>Sign in
					</a>
				</li>

				<li class="nav-item">
					<a
						rel="prefetch"
						href="/register"
						class:active={$page.path === '/register'}>
						<svg class="icon"><use xlink:href="/svg/icons.svg#gg-user-add"/></svg>Sign up
					</a>
				</li>
			{/if}
		</ul>
	</div>
</nav>

<style>

	h1 {
		font-family: 'Anonymous Pro', monospace;
		padding: 20px;
		width: 100%;
	}
	
	a {
  		color: inherit; 
  		text-decoration: inherit; 
	}

	p {
		display:  inline;
	}
	
	.left-logo {
		color: var(--primary-color);
		font-weight: 500;		
	}

	.right-logo {
		color: var(--secondary-color);
		font-weight: 700;
	}

	.icon {
		color: var(--primary-color);
		padding: 4px;
		width: 22px;
		height: 22px;
		margin-bottom: 5px;
		margin-right: 2px;
	}

	nav {
		background-color: var(--off-white);
		font-family: "Helvetica Neue", "Helvetica", "Arial", sans-serif;
		height: 50px;
		border-bottom: 2px solid var(--primary-color);
	}

	.inner {
		max-width: 980px;
		padding-left: 20px;
		padding-right: 20px;
		margin: auto;
		box-sizing: border-box;
		display: flex;
		align-items: center;
		height: 100%;
	}

	.mobile-icon {
		width: 25px;
		height: 14px;
		position: relative;
		cursor: pointer;
	}

	.mobile-icon:after,
	.mobile-icon:before,
	.middle-line {
		content: "";
		position: absolute;
		width: 66%;
		height: 2px;
		background-color: var(--tertiary-color);
		transition: all 0.4s;
		transform-origin: center;
	}

	.mobile-icon:before,
	.middle-line {
		top: 0;
	}

	.mobile-icon:after,
	.middle-line {
		bottom: 0;
	}

	.mobile-icon:before {
		width: 33%;
		background-color: var(--secondary-color);
	}

	.mobile-icon:after {
		width: 100%;
		background-color: var(--primary-color);
	}

	.middle-line {
		margin: auto;
	}

	.mobile-icon:hover:before,
	.mobile-icon:hover:after,
	.mobile-icon.active:before,
	.mobile-icon.active:after,
	.mobile-icon.active .middle-line {
		width: 100%;
		background-color: var(--primary-color);
	}
	.mobile-icon.active:before {
		top: 50%;
		transform: rotate(45deg);
		background-color: var(--primary-color);
	}
	.mobile-icon.active:after {
		top: 50%;
		transform: rotate(-45deg);
		background-color: var(--primary-color);
	}
	.mobile-icon:hover .middle-line {
		width: 100%;
		background-color: var(--primary-color);
	}
	.mobile-icon.active .middle-line {
		width: 0%;
	}

	.navbar-list {
		font-family: 'Anonymous Pro', monospace;
		font-size: 20px;
		display: none;
		padding: 0 20px;
		margin-left: auto;
	}

	.navbar-list.mobile {
		background-color: var(--secondary-color);		
		position: fixed;
		display: block;
		z-index: 1;
		height: calc(100% - 52px);
		margin-bottom: 0px;
		bottom: 0;
		left: 0;
	}

	.navbar-list li {
		list-style-type: none;
		position: relative;
	}

	.navbar-list a {
		color: var(--off-white);
		text-decoration: none;
		display: flex;
		height: 45px;
		align-items: center;
		padding: 0 5px;
	}

	.navbar-list p {
		color: var(--off-white);
		text-decoration: none;
		height: 45px;
		align-items: center;
		padding: 0 5px;
	}

	.navbar-list a.active {
		text-decoration: underline;
	}

	@media only screen and (min-width: 767px) {
		h1 {
			width: auto;
		}
		.mobile-icon {
			display: none;
		}

		.navbar-list {
			display: flex;
		}

		.navbar-list a {
			display: inline-flex;
			color: var(--secondary-color);			
		}
	}
</style>
