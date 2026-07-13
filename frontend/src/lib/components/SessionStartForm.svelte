<script lang="ts">
	import type { ProductionSession } from '$lib/types/ProductionSession.ts';

	let { onSessionStarted } = $props<{
		onSessionStarted: (productionSession: ProductionSession) => void;
	}>();

	let selectedAnimalKind = $state<string>('');

	const station = {
		name: 'Grossvieh Linie 1',
		location: 'Schlachthalle A'
	};

	const availableAnimalKinds = [
		{ value: 'Kalb', label: 'Kalb' },
		{ value: 'Rind', label: 'Rind' },
		{ value: 'Kuh', label: 'Kuh' },
		{ value: 'Muni', label: 'Muni' },
		{ value: 'Ochse', label: 'Ochse' }
	];

	function startSession() {
		const productionSession: ProductionSession = {
			id: crypto.randomUUID(),
			station: station.name,
			animalKind: selectedAnimalKind,
			startedAt: new Date().toISOString()
		};

		onSessionStarted(productionSession);
	}
</script>

<main class="min-h-screen bg-slate-100 p-6 text-slate-900 dark:bg-slate-950 dark:text-slate-100">
	<section class="mx-auto grid max-w-4xl gap-6">
		<header
			class="rounded-3xl border border-slate-300 bg-white p-6 shadow-sm dark:border-slate-700 dark:bg-slate-900"
		>
			<p class="text-sm font-bold uppercase tracking-wider text-cyan-700 dark:text-cyan-300">
				Station
			</p>

			<h1 class="mt-2 text-4xl font-bold">
				{station.name}
			</h1>

			<p class="mt-2 text-slate-600 dark:text-slate-300">
				{station.location}
			</p>
		</header>

		<section
			class="rounded-3xl border border-slate-300 bg-white p-6 shadow-sm dark:border-slate-700 dark:bg-slate-900"
		>
			<h2 class="text-2xl font-bold">Keine aktive Session</h2>

			<p class="mt-2 text-slate-600 dark:text-slate-300">
				Starten Sie eine neue Produktionssession und wählen Sie die aktuelle Tierart.
			</p>

			<div class="mt-6 grid gap-3">
				<label for="animal-kind-select" class="text-sm font-bold"> Aktuelle Tierart </label>

				<select
					id="animal-kind-select"
					bind:value={selectedAnimalKind}
					class="h-14 rounded-xl border border-slate-400 bg-white px-4 text-lg dark:border-slate-600 dark:bg-slate-950"
				>
					<option value="" disabled> Tierart auswählen </option>

					{#each availableAnimalKinds as animal (animal.value)}
						<option value={animal.value}>
							{animal.label}
						</option>
					{/each}
				</select>
			</div>

			<button
				onclick={startSession}
				disabled={!selectedAnimalKind}
				class="mt-8 h-20 w-full rounded-2xl bg-cyan-700 text-2xl font-bold text-white disabled:cursor-not-allowed disabled:opacity-40"
			>
				Session starten
			</button>
		</section>

		<section
			class="rounded-3xl border border-slate-300 bg-white p-5 text-sm dark:border-slate-700 dark:bg-slate-900"
		>
			<div class="flex justify-between">
				<span class="font-semibold">Benutzer</span>
				<span>Max Muster</span>
			</div>

			<div class="mt-2 flex justify-between">
				<span class="font-semibold">Rolle</span>
				<span>Operator</span>
			</div>
		</section>
	</section>
</main>
