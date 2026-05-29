import { env } from '$env/dynamic/public';

/**
 * Hilfsfunktion für API-Requests
 */
export async function api<T>(path: string, options?: RequestInit): Promise<T> {
	const baseUrl = env.PUBLIC_API_BASE_URL;
	if (!baseUrl) {
		throw new Error('Missing PUBLIC_API_BASE_URL environment variable.');
	}

	const url = `${baseUrl}${path}`;

	const res = await fetch(url, {
		headers: {
			'Content-Type': 'application/json',
			...(options?.headers ?? {})
		},
		...options
	});

	if (!res.ok) {
		throw new Error(await res.text());
	}

	return res.json() as Promise<T>;
}
