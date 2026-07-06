<script lang="ts">
	import { resolve } from '$app/paths';
	import LL from '../../i18n/i18n-svelte';
	import LanguageSwitcher from '$lib/components/LanguageSwitcher.svelte';
	import ThemeToggle from '$lib/components/ThemeToggle.svelte';
</script>

<svelte:head>
	<title>{$LL.login.title()}</title>
</svelte:head>

<main
	class="relative grid min-h-screen place-items-center overflow-hidden px-4 py-6 text-slate-900 dark:text-slate-100 sm:px-6 sm:py-8 [@media(max-height:800px)]:py-3"
>
	<div
		aria-hidden="true"
		class="pointer-events-none absolute left-1/2 top-24 h-[58vh] w-[min(980px,90vw)] -translate-x-1/2 -rotate-3 rounded-[42px] bg-gradient-to-br from-sky-900/15 to-cyan-500/10 blur-[2px] dark:from-cyan-200/15 dark:to-sky-300/10"
	></div>

	<div class="absolute right-4 top-4 z-20 flex items-center gap-2 sm:right-6 sm:top-6">
		<ThemeToggle />
		<LanguageSwitcher />
	</div>

	<section
		aria-label="SomnosSuite Login"
		class="relative mx-auto my-auto grid w-full max-w-6xl items-center gap-6 lg:grid-cols-[1.05fr_minmax(320px,460px)] [@media(max-height:800px)]:gap-4"
	>
		<div
			class="grid content-center gap-4 px-1 py-2 lg:p-6 [@media(max-height:800px)]:gap-2 [@media(max-height:800px)]:py-1"
		>
			<p
				class="m-0 text-xs font-bold uppercase tracking-[0.18em] text-cyan-900 dark:text-cyan-200"
			>
				{$LL.login.internalLabel()}
			</p>
			<h1
				class="m-0 max-w-[19ch] font-serif text-4xl leading-[0.95] text-slate-900 dark:text-slate-100 sm:text-5xl lg:text-6xl [@media(max-height:800px)]:lg:text-5xl"
			>
				{$LL.login.headline()}
			</h1>
			<p
				class="m-0 max-w-2xl text-base leading-relaxed text-slate-800 dark:text-slate-200 sm:text-lg"
			>
				{$LL.login.subline()}
			</p>

			<ul class="mt-3 grid gap-3">
				<li class="flex items-center gap-3 text-sm text-slate-800 dark:text-slate-200">
					<span
						aria-hidden="true"
						class="h-2.5 w-2.5 shrink-0 rounded-full bg-gradient-to-br from-cyan-400 to-sky-600 ring-4 ring-cyan-300/30 dark:ring-cyan-200/20"
					></span>
					<span>{$LL.login.benefitSecure()}</span>
				</li>
				<li class="flex items-center gap-3 text-sm text-slate-800 dark:text-slate-200">
					<span
						aria-hidden="true"
						class="h-2.5 w-2.5 shrink-0 rounded-full bg-gradient-to-br from-cyan-400 to-sky-600 ring-4 ring-cyan-300/30 dark:ring-cyan-200/20"
					></span>
					<span>{$LL.login.benefitDashboards()}</span>
				</li>
				<li class="flex items-center gap-3 text-sm text-slate-800 dark:text-slate-200">
					<span
						aria-hidden="true"
						class="h-2.5 w-2.5 shrink-0 rounded-full bg-gradient-to-br from-cyan-400 to-sky-600 ring-4 ring-cyan-300/30 dark:ring-cyan-200/20"
					></span>
					<span>{$LL.login.benefitTriage()}</span>
				</li>
			</ul>
		</div>

		<div class="grid items-center">
			<article
				aria-label="Sign in form"
				class="rounded-3xl border border-slate-900/15 bg-white/95 p-5 shadow-[0_26px_56px_rgba(15,23,42,0.12),0_3px_10px_rgba(15,23,42,0.08)] backdrop-blur-xl dark:border-slate-200/15 dark:bg-slate-950/95 dark:shadow-[0_30px_80px_rgba(2,6,23,0.65),0_2px_8px_rgba(14,165,233,0.15)] sm:p-7 [@media(max-height:800px)]:sm:p-5"
			>
				<header>
					<div class="mb-3 flex items-center justify-between gap-3">
						<span
							title="Current environment"
							class="inline-flex items-center rounded-full bg-cyan-900 px-2.5 py-1 text-[11px] font-bold uppercase tracking-wide text-white dark:bg-cyan-200 dark:text-slate-950"
						>
							{$LL.login.envLabel()}
						</span>
						<span
							class="inline-flex items-center rounded-full bg-slate-800 px-2.5 py-1 text-[11px] font-bold uppercase tracking-wide text-white dark:bg-slate-200 dark:text-slate-950"
						>
							{$LL.login.buildLabel()}
						</span>
					</div>
					<h2 class="m-0 font-serif text-4xl text-slate-900 dark:text-slate-100">
						{$LL.login.signInHeading()}
					</h2>
					<p class="mb-5 mt-1 text-sm text-slate-700 dark:text-slate-300">
						{$LL.login.signInSubline()}
					</p>
				</header>

				<form method="post" action="/login" class="grid gap-2.5">
					<label
						for="email"
						class="text-xs font-bold tracking-wide text-slate-800 dark:text-slate-200"
					>
						{$LL.login.workEmailLabel()}
					</label>
					<!-- svelte-ignore a11y_autofocus -->
					<input
						id="email"
						type="email"
						name="email"
						autocomplete="email"
						placeholder={$LL.login.workEmailPlaceholder()}
						required
						autofocus
						class="h-11 w-full rounded-xl border border-slate-400 bg-white px-3 text-sm text-slate-950 placeholder:text-slate-600 focus:border-cyan-900 focus:ring-0 dark:border-slate-500 dark:bg-slate-950 dark:text-slate-100 dark:placeholder:text-slate-300 dark:focus:border-cyan-300"
					/>

					<label
						for="password"
						class="text-xs font-bold tracking-wide text-slate-800 dark:text-slate-200"
					>
						{$LL.login.passwordLabel()}
					</label>
					<input
						id="password"
						type="password"
						name="password"
						autocomplete="current-password"
						placeholder={$LL.login.passwordPlaceholder()}
						required
						class="h-11 w-full rounded-xl border border-slate-400 bg-white px-3 text-sm text-slate-950 placeholder:text-slate-600 focus:border-cyan-900 focus:ring-0 dark:border-slate-500 dark:bg-slate-950 dark:text-slate-100 dark:placeholder:text-slate-300 dark:focus:border-cyan-300"
					/>

					<div class="mb-1 mt-1 flex items-center justify-between gap-3">
						<label
							for="remember"
							class="inline-flex items-center gap-2 text-xs font-semibold text-slate-800 dark:text-slate-200"
						>
							<input
								id="remember"
								type="checkbox"
								name="remember"
								class="h-4 w-4 rounded border-slate-400 text-cyan-700 accent-cyan-700 focus:ring-cyan-700 dark:border-slate-500 dark:bg-slate-900 dark:focus:ring-cyan-400"
							/>
							<span>{$LL.login.rememberDevice()}</span>
						</label>
						<a
							href={resolve('/about')}
							class="text-xs font-semibold text-cyan-900 hover:underline dark:text-cyan-200"
						>
							{$LL.login.forgotPassword()}
						</a>
					</div>

					<button
						type="submit"
						class="mt-1 h-11 rounded-xl bg-gradient-to-r from-cyan-700 to-sky-700 text-sm font-bold text-white transition-transform hover:-translate-y-0.5"
					>
						{$LL.login.signInButton()}
					</button>
				</form>

				<div
					aria-hidden="true"
					class="my-4 grid grid-cols-[1fr_auto_1fr] items-center gap-3"
				>
					<span class="h-px bg-slate-300 dark:bg-slate-700"></span>
					<p
						class="m-0 text-[11px] font-bold uppercase tracking-[0.09em] text-slate-700 dark:text-slate-300"
					>
						{$LL.login.orLabel()}
					</p>
					<span class="h-px bg-slate-300 dark:bg-slate-700"></span>
				</div>

				<button
					type="button"
					class="h-11 w-full rounded-xl border border-slate-400 bg-white text-sm font-bold text-slate-900 hover:bg-slate-100 dark:border-slate-500 dark:bg-slate-900 dark:text-slate-100 dark:hover:bg-slate-800"
				>
					{$LL.login.ssoButton()}
				</button>

				<footer class="mt-4 flex flex-wrap items-center justify-between gap-2">
					<a
						href={resolve('/about')}
						class="text-xs font-semibold text-cyan-900 hover:underline dark:text-cyan-200"
					>
						{$LL.login.needAccess()}
					</a>
					<a
						href={resolve('/about')}
						class="text-xs font-semibold text-cyan-900 hover:underline dark:text-cyan-200"
					>
						{$LL.login.systemStatus()}
					</a>
				</footer>
			</article>
		</div>
	</section>
</main>
