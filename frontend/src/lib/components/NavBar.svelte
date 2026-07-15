<script lang="ts">
	import { resolve } from '$app/paths';
	import favicon from '$lib/assets/_favicon.svg';
	import ThemeToggle from '$lib/components/ui/theme-toggle/theme-toggle.svelte';
	import UserMenu from '$lib/components/ui/user-menu/user-menu.svelte';
	import type { UserMenuItem } from '$lib/components/ui/user-menu/user-menu.svelte';
	import LanguageSwitcher from '$lib/components/ui/language-switcher/app-language-switcher.svelte';
	import SettingsIcon from '@lucide/svelte/icons/settings';
	import LogOutIcon from '@lucide/svelte/icons/log-out';

	export let siteName = 'SomnosSuite';
	/** @type {'/' | '/dashboard'} */
	export let href: '/' | '/dashboard' = '/dashboard';

	const items: UserMenuItem[] = [
		{
			label: 'Settings',
			icon: SettingsIcon,
			onSelect: () => console.log('settings')
		},
		{
			label: 'Log out',
			icon: LogOutIcon,
			separator: true,
			onSelect: () => console.log('logout')
		}
	];
</script>

<nav
	aria-label="Main Navigation"
	class="relative flex w-full flex-wrap items-center justify-between border-b border-slate-900/15 bg-white/95 py-2 shadow-[0_10px_24px_rgba(15,23,42,0.12),0_2px_8px_rgba(15,23,42,0.08)] backdrop-blur-xl dark:border-slate-200/15 dark:bg-slate-950/95 dark:shadow-[0_14px_30px_rgba(2,6,23,0.6),0_2px_8px_rgba(14,165,233,0.15)] lg:py-4"
>
	<div class="flex w-full flex-wrap items-center justify-between gap-3 px-3">
		<!-- Logo -->
		<a class="mx-2 my-1 flex items-center lg:mt-0 lg:mb-0" href={resolve(href)}>
			<img src={favicon} class="me-2 h-10" alt="Logo" loading="lazy" />
			<span class="font-semibold text-slate-900 dark:text-slate-100">{siteName}</span>
		</a>

		<!-- Navigation links -->
		<ul class="list-style-none me-auto flex flex-col ps-0 lg:flex-row" data-twe-navbar-nav-ref>
			<li class="mb-4 lg:mb-0 lg:pe-2" data-twe-nav-item-ref>
				<a
					class="text-slate-700 transition duration-200 hover:text-slate-900 focus:text-slate-900 active:text-slate-900 lg:px-2 dark:text-slate-300 dark:hover:text-slate-100 dark:focus:text-slate-100 dark:active:text-slate-100"
					href={resolve('/dashboard')}
					data-twe-nav-link-ref>Dashboard</a
				>
			</li>
			<li class="mb-4 lg:mb-0 lg:pe-2" data-twe-nav-item-ref>
				<a
					class="text-slate-700 transition duration-200 hover:text-slate-900 focus:text-slate-900 active:text-slate-900 lg:px-2 dark:text-slate-300 dark:hover:text-slate-100 dark:focus:text-slate-100 dark:active:text-slate-100"
					href={resolve('/about')}
					data-twe-nav-link-ref>About</a
				>
			</li>
		</ul>

		<ThemeToggle />
		<LanguageSwitcher />
		<UserMenu name="Max Mustermann" fallback="MM" image="" {items} />
	</div>
</nav>
