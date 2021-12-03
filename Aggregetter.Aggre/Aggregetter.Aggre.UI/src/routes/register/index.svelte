<script context="module">
	export async function load({ session }) {
		if (session.user) {
			return { redirect: '/', status: 302 };
		}
		return {};
	}
</script>

<script>
	import { session } from '$app/stores';
	import { goto } from '$app/navigation';
	import { post } from '$lib/utils/post';
	import ListErrors from '$lib/errors/ListErrors.svelte';

	let username = '';
	let email = '';
	let password = '';
	let errors = null;

	async function submit(event) {
		const response = await post(`auth/register`, { email, password, username });

		// TODO handle network errors
		errors = response.errors;
        console.log(response);
		if (response.status == 200) {
			//$session.user = response;
			goto('/login');
		}
	}
</script>

<svelte:head>
	<title>Sign up</title>
</svelte:head>

<section>
	<hgroup>
	    <h1>Sign Up</h1>
	</hgroup>

	<ListErrors {errors}/>

	<form on:submit|preventDefault={submit} class="form">

	    <div class="form-input">
	    	<input type="text" id="username" name="username" placeholder="Username" bind:value={username}/>
	    </div>

		<div class="form-input">
	    	<input type="email" id="email" name="email" placeholder="Email" bind:value={email}/>
	    </div>

	    <div class="form-input">
	        <input type="password" id="password" name="password" placeholder="Password" bind:value={password}/>
	    </div>

	    <a class="link" href="/login">Already registered?</a>

	    <br><br>

		<div class="container-form-button">
          <button type="submit" name="button_submit" class="button">
            <span>Register</span>
          </button>
        </div>
	</form>
</section>



<style>
	
	* { box-sizing:border-box; }

	hgroup { 
		text-align:center;
	}

	span {
		font-size: 0.95em;
		line-height: 24px;
	}

	h1 {
		color: var(--tertiary-color);
		font-size: 30px;
	}
	section {
		flex: 1;
		background-image: url(svg/computer.svg);
		background-repeat: no-repeat;
		background-position: center;
		background-position-y: 200px;
		height: 100%;
	}

	@media only screen and (min-width: 767px) {
		section {
			background-size: 100%;
			background-position-y: 180px;
		}
	}

	.button {
		width: 200px;
	}
</style>