<script lang="ts">
	import type { ProductionSession } from '$lib/types/ProductionSession';

	let { productionSession } = $props<{
		productionSession: ProductionSession;
	}>();

	type Animal = {
		id: string;
		earTag: string;
		supplier?: string;
		animalKind: string;
	};

	let currentAnimal: Animal = $derived({
		id: '1',
		earTag: '756001234567',
		supplier: 'Musterhof',
		animalKind: productionSession.animalKind
	});

	let queue: Animal[] = $derived([
		{
			id: '2',
			earTag: '756001234568',
			supplier: 'Hof A',
			animalKind: productionSession.animalKind
		},
		{
			id: '3',
			earTag: '756001234569',
			supplier: 'Hof B',
			animalKind: productionSession.animalKind
		}
	]);

	function recordOutcome(outcome: 'Good' | 'Bad') {
		console.log({
			animal: currentAnimal,
			outcome
		});
	}
</script>

<main
	class="grid min-h-screen gap-6 bg-slate-100 p-6 text-slate-900 dark:bg-slate-950 dark:text-slate-100"
>
	<header class="rounded-3xl bg-white p-6 shadow dark:bg-slate-900">
		<p class="text-sm font-bold uppercase tracking-wide text-cyan-700">Station</p>

		<h1 class="text-4xl font-bold">
			{productionSession.station}
		</h1>

		<p class="mt-2">
			Session: {productionSession.animalKind}
		</p>
	</header>

	<section class="rounded-3xl bg-white p-8 shadow dark:bg-slate-900">
		<h2 class="text-2xl font-bold">Aktuelles Tier</h2>

		<div class="mt-6 grid gap-4 text-xl">
			<div>
				<span class="font-bold"> Ohrmarke </span>

				<p>
					{currentAnimal.earTag}
				</p>
			</div>

			<div>
				<span class="font-bold"> Lieferant </span>

				<p>
					{currentAnimal.supplier}
				</p>
			</div>

			<div>
				<span class="font-bold"> Tierart </span>

				<p>
					{currentAnimal.animalKind}
				</p>
			</div>
		</div>

		<div class="mt-10 grid grid-cols-2 gap-6">
			<button
				onclick={() => recordOutcome('Good')}
				class="h-32 rounded-3xl bg-green-700 text-4xl font-bold text-white"
			>
				GOOD
			</button>

			<button
				onclick={() => recordOutcome('Bad')}
				class="h-32 rounded-3xl bg-red-700 text-4xl font-bold text-white"
			>
				BAD
			</button>
		</div>
	</section>

	<section class="rounded-3xl bg-white p-6 shadow dark:bg-slate-900">
		<h2 class="text-xl font-bold">Nächste Tiere</h2>

		<table class="mt-4 w-full">
			<thead>
				<tr class="text-left">
					<th>Ohrmarke</th>
					<th>Tierart</th>
				</tr>
			</thead>

			<tbody>
				{#each queue as animal (animal.id)}
					<tr>
						<td>{animal.earTag}</td>
						<td>{animal.animalKind}</td>
					</tr>
				{/each}
			</tbody>
		</table>
	</section>

	<button class="h-20 rounded-3xl bg-slate-800 text-2xl font-bold text-white">
		Tagesabschluss
	</button>
</main>
