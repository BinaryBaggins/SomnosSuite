<script lang="ts">
	import '../app.css';
	import { browser } from '$app/environment';
	import { page } from '$app/stores';
	import { setLocale } from '../i18n/i18n-svelte';
	import { loadLocale } from '../i18n/i18n-util.sync';
	import { defaultLocale, getPreferredLocale, persistLocale } from '$lib/i18n/locale';
	import favicon from '$lib/assets/_favicon.svg';
	import NavBar from '$lib/components/NavBar.svelte';

	const initialLocale = browser ? getPreferredLocale() : defaultLocale;

	loadLocale(initialLocale);
	setLocale(initialLocale);

	if (browser) {
		persistLocale(initialLocale);
	}

	let { children } = $props();
</script>

<svelte:head>
	<link rel="icon" href={favicon} />
</svelte:head>

{#if $page.url.pathname !== '/login'}
	<NavBar></NavBar>
{/if}
{@render children?.()}
