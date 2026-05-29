<script>
	import { resolve } from '$app/paths';
	import favicon from '$lib/assets/_favicon.svg';

	export let siteName = 'SomnosSuite';
	/** @type {'/' | '/about'} */
	export let href = '/';
</script>

<nav
	aria-label="Main Navigation"
	class="shadow-dark-mild relative flex w-full flex-wrap items-center justify-between bg-zinc-50 py-2 lg:py-4 dark:bg-neutral-700"
>
	<div class="flex w-full flex-wrap items-center justify-between px-3">
		<!-- Logo -->
		<a class="mx-2 my-1 flex items-center lg:mt-0 lg:mb-0" href={resolve(href)}>
			<img src={favicon} class="me-2 h-10" alt="Logo" loading="lazy" />
			<span class="text-black dark:text-white">{siteName}</span>
		</a>

		<!-- Navigation links -->
		<ul class="list-style-none me-auto flex flex-col ps-0 lg:flex-row" data-twe-navbar-nav-ref>
			<li class="mb-4 lg:mb-0 lg:pe-2" data-twe-nav-item-ref>
				<a
					class="text-black/60 transition duration-200 hover:text-black/80 focus:text-black/80 active:text-black/80 lg:px-2 dark:text-white/60 dark:hover:text-white/80"
					href={resolve('/')}
					data-twe-nav-link-ref>Overview</a
				>
			</li>
			<li class="mb-4 lg:mb-0 lg:pe-2" data-twe-nav-item-ref>
				<a
					class="text-black/60 transition duration-200 hover:text-black/80 focus:text-black/80 active:text-black/80 lg:px-2 dark:text-white/60 dark:hover:text-white/80"
					href={resolve('/about')}
					data-twe-nav-link-ref>About</a
				>
			</li>
		</ul>
	</div>
</nav>
