<script lang="ts">
	import { onMount } from 'svelte';
	import { api } from '$lib/api';

	type Forecast = {
		date: string;
		temperatureC: number;
		temperatureF: number;
		summary: string;
	};

	let forecasts: Forecast[] = [];
	let error = '';

	// Form-Felder
	let date = '';
	let temperatureC: number | null = null;
	let summary = '';

	let formError = '';
	let formSuccess = '';

	async function loadForecasts() {
		try {
			forecasts = await api<Forecast[]>('/WeatherForecast');
		} catch (e) {
			error = e instanceof Error ? e.message : 'Unknown error';
		}
	}

	onMount(() => {
		loadForecasts();
	});

	async function submitForecast() {
		formError = '';
		formSuccess = '';

		if (!date || temperatureC === null || !summary) {
			formError = 'Alle Felder müssen ausgefüllt werden!';
			return;
		}

		try {
			const newForecast = {
				date,
				temperatureC,
				temperatureF: Math.round((temperatureC * 9) / 5 + 32),
				summary
			};

			await api('/WeatherForecast', {
				method: 'POST',
				body: JSON.stringify(newForecast)
			});

			formSuccess = 'Forecast erfolgreich erstellt!';
			// Felder leeren
			date = '';
			temperatureC = null;
			summary = '';

			// Liste aktualisieren
			await loadForecasts();
		} catch (err) {
			formError = err instanceof Error ? err.message : 'Unknown error';
		}
	}
</script>

<h1 class="mb-4 text-2xl font-bold">Weather Forecast</h1>

<!-- Fehler bei GET -->
{#if error}
	<p class="text-red-600">{error}</p>
{/if}

<!-- Formular -->
<div class="mb-6 rounded border bg-gray-50 p-4">
	{#if formError}
		<p class="text-red-600">{formError}</p>
	{/if}
	{#if formSuccess}
		<p class="text-green-600">{formSuccess}</p>
	{/if}

	<form class="space-y-4" on:submit|preventDefault={submitForecast}>
		<div>
			<label class="mb-1 block font-medium"
				>Datum
				<input type="date" bind:value={date} class="w-full rounded border p-2" /></label
			>
		</div>

		<div>
			<label class="mb-1 block font-medium"
				>Temperatur (°C)
				<input
					type="number"
					bind:value={temperatureC}
					class="w-full rounded border p-2"
				/></label
			>
		</div>

		<div>
			<label class="mb-1 block font-medium"
				>Summary
				<input type="text" bind:value={summary} class="w-full rounded border p-2" /></label
			>
		</div>

		<button type="submit" class="rounded bg-blue-600 px-4 py-2 text-white hover:bg-blue-700">
			Forecast erstellen
		</button>
	</form>
</div>

<!-- Liste -->
{#if forecasts.length === 0}
	<p class="opacity-70">Lade Daten…</p>
{:else}
	<ul class="grid gap-3">
		{#each forecasts as f (f.date)}
			<li class="rounded-2xl bg-white p-4 shadow">
				<div class="font-medium">{new Date(f.date).toLocaleDateString()}</div>
				<div class="text-sm">🌡️ {f.temperatureC} °C / {f.temperatureF} °F</div>
				<div class="text-sm opacity-70">{f.summary}</div>
			</li>
		{/each}
	</ul>
{/if}
