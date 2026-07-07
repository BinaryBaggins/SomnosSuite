<script lang="ts">
	export let name = 'Jese Leos';
	export let role = 'Role';
	export let src: string | undefined = undefined;
	export let alt: string | undefined = undefined;
	export let href: string | undefined = undefined;

	function getInitials(value: string) {
		return value
			.trim()
			.split(/\s+/)
			.filter(Boolean)
			.slice(0, 2)
			.map((part) => part[0]?.toUpperCase() ?? '')
			.join('');
	}

	$: initials = getInitials(name) || 'U';
</script>

{#if href}
	<a
		{href}
		class="inline-flex items-center gap-2.5 rounded-xl border border-slate-900/15 bg-white/95 px-2.5 py-2 text-slate-900 shadow-[0_10px_24px_rgba(15,23,42,0.12),0_2px_8px_rgba(15,23,42,0.08)] backdrop-blur-xl transition hover:bg-slate-100 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-cyan-900 dark:border-slate-200/15 dark:bg-slate-950/95 dark:text-slate-100 dark:shadow-[0_14px_30px_rgba(2,6,23,0.6),0_2px_8px_rgba(14,165,233,0.15)] dark:hover:bg-slate-800 dark:focus-visible:outline-cyan-300"
		aria-label={role ? `${name}, ${role}` : name}
	>
		<div
			class="relative flex h-10 w-10 shrink-0 items-center justify-center overflow-hidden rounded-full bg-neutral-secondary-medium"
		>
			{#if src}
				<img {src} alt={alt ?? name} class="h-full w-full object-cover" loading="lazy" />
			{:else}
				<div
					class="flex h-full w-full items-center justify-center text-sm font-semibold uppercase text-body-subtle"
					aria-hidden="true"
				>
					{initials}
				</div>
			{/if}
		</div>
		<div class="min-w-0 font-medium text-heading">
			<div class="truncate">{name}</div>
			{#if role}
				<div class="truncate text-sm font-normal text-body">{role}</div>
			{/if}
		</div>
	</a>
{:else}
	<div
		class="inline-flex items-center gap-2.5 rounded-xl border border-slate-900/15 bg-white/95 px-2.5 py-2 text-slate-900 shadow-[0_10px_24px_rgba(15,23,42,0.12),0_2px_8px_rgba(15,23,42,0.08)] backdrop-blur-xl transition hover:bg-slate-100 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-cyan-900 dark:border-slate-200/15 dark:bg-slate-950/95 dark:text-slate-100 dark:shadow-[0_14px_30px_rgba(2,6,23,0.6),0_2px_8px_rgba(14,165,233,0.15)] dark:hover:bg-slate-800 dark:focus-visible:outline-cyan-300"
		aria-label={role ? `${name}, ${role}` : name}
	>
		<div
			class="relative flex h-10 w-10 shrink-0 items-center justify-center overflow-hidden rounded-full bg-neutral-secondary-medium"
		>
			{#if src}
				<img {src} alt={alt ?? name} class="h-full w-full object-cover" loading="lazy" />
			{:else}
				<div
					class="flex h-full w-full items-center justify-center text-sm font-semibold uppercase text-body-subtle"
					aria-hidden="true"
				>
					{initials}
				</div>
			{/if}
		</div>
		<div class="min-w-0 font-medium text-heading">
			<div class="truncate">{name}</div>
			{#if role}
				<div class="truncate text-sm font-normal text-body">{role}</div>
			{/if}
		</div>
	</div>
{/if}
