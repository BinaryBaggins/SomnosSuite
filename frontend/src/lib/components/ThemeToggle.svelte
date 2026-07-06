<script lang="ts">
	import { browser } from '$app/environment';
	import darkModeIcon from '$lib/assets/asleep-filled-svgrepo-com.svg';
	import lightModeIcon from '$lib/assets/sunny-svgrepo-com.svg';

	type Theme = 'light' | 'dark';
	const THEME_STORAGE_KEY = 'somnos-theme';

	let theme = $state<Theme>('light');

	function getInitialTheme(): Theme {
		if (!browser) {
			return 'light';
		}

		const storedTheme = localStorage.getItem(THEME_STORAGE_KEY);
		if (storedTheme === 'light' || storedTheme === 'dark') {
			return storedTheme;
		}

		return window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
	}

	function applyTheme(nextTheme: Theme) {
		theme = nextTheme;

		if (!browser) {
			return;
		}

		document.documentElement.classList.toggle('dark', nextTheme === 'dark');
		localStorage.setItem(THEME_STORAGE_KEY, nextTheme);
	}

	function toggleTheme() {
		applyTheme(theme === 'dark' ? 'light' : 'dark');
	}

	if (browser) {
		applyTheme(getInitialTheme());
	}
</script>

<button
	type="button"
	onclick={toggleTheme}
	class="inline-flex h-10 w-10 items-center justify-center rounded-xl border border-slate-900/15 bg-white/95 text-slate-900 shadow-[0_10px_24px_rgba(15,23,42,0.12),0_2px_8px_rgba(15,23,42,0.08)] backdrop-blur-xl transition hover:bg-slate-100 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-cyan-900 dark:border-slate-200/15 dark:bg-slate-950/95 dark:text-slate-100 dark:shadow-[0_14px_30px_rgba(2,6,23,0.6),0_2px_8px_rgba(14,165,233,0.15)] dark:hover:bg-slate-800 dark:focus-visible:outline-cyan-300"
	aria-label={theme === 'dark' ? 'Switch to light mode' : 'Switch to dark mode'}
	title={theme === 'dark' ? 'Switch to light mode' : 'Switch to dark mode'}
>
	{#if theme === 'dark'}
		<img src={lightModeIcon} class="h-5 w-5" alt="" aria-hidden="true" />
	{:else}
		<img src={darkModeIcon} class="h-5 w-5" alt="" aria-hidden="true" />
	{/if}
</button>
