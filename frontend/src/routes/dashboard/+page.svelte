<script lang="ts">
	import { resolve } from '$app/paths';
	import LL from '../../i18n/i18n-svelte';
	import LanguageSwitcher from '$lib/components/LanguageSwitcher.svelte';
</script>

<svelte:head>
	<title>{$LL.dashboard.title()}</title>
</svelte:head>
