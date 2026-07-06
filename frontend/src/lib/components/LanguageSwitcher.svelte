<script lang="ts">
	import LL, { locale, setLocale } from '../../i18n/i18n-svelte';
	import { loadLocale } from '../../i18n/i18n-util.sync';
	import type { Locales } from '../../i18n/i18n-types';
	import { persistLocale } from '../i18n/locale';

	const availableLocales: Locales[] = ['en', 'de'];

	const handleLanguageChange = (event: Event) => {
		const nextLocale = (event.currentTarget as HTMLSelectElement).value as Locales;
		if (!availableLocales.includes(nextLocale)) return;

		loadLocale(nextLocale);
		setLocale(nextLocale);
		persistLocale(nextLocale);
	};
</script>

<div
	class="inline-flex items-center gap-2 rounded-xl border border-slate-900/15 bg-white/95 px-2.5 py-1.5 shadow-[0_10px_24px_rgba(15,23,42,0.12),0_2px_8px_rgba(15,23,42,0.08)] backdrop-blur-xl dark:border-slate-200/15 dark:bg-slate-950/95 dark:shadow-[0_14px_30px_rgba(2,6,23,0.6),0_2px_8px_rgba(14,165,233,0.15)]"
>
	<label
		for="language"
		class="text-[11px] font-bold uppercase tracking-[0.08em] text-slate-700 dark:text-slate-200"
	>
		{$LL.common.languageLabel()}
	</label>
	<div class="relative inline-block">
		<select
			id="language"
			class="inline-block w-fit min-w-0 appearance-none rounded-md border border-slate-400 bg-white bg-none py-1 pl-2 pr-7 text-xs font-semibold text-slate-900 focus:border-cyan-900 focus:ring-0 dark:border-slate-500 dark:bg-slate-950 dark:text-slate-100 dark:focus:border-cyan-300"
			on:change={handleLanguageChange}
			value={$locale}
		>
			{#each availableLocales as code (code)}
				<option value={code}>
					{code === 'en' ? $LL.common.localeOptionEn() : $LL.common.localeOptionDe()}
				</option>
			{/each}
		</select>
		<svg
			aria-hidden="true"
			viewBox="0 0 20 20"
			fill="currentColor"
			class="pointer-events-none absolute right-2 top-1/2 h-3.5 w-3.5 -translate-y-1/2 text-slate-600 dark:text-slate-300"
		>
			<path
				fill-rule="evenodd"
				d="M5.23 7.21a.75.75 0 011.06.02L10 11.168l3.71-3.938a.75.75 0 111.08 1.04l-4.25 4.51a.75.75 0 01-1.08 0l-4.25-4.51a.75.75 0 01.02-1.06z"
				clip-rule="evenodd"
			/>
		</svg>
	</div>
</div>
