import { PUBLIC_API_BASE_URL } from '$env/static/public';

/**
 * Hilfsfunktion für API-Requests
 */
export async function api<T>(path: string, options?: RequestInit): Promise<T> {
	const url = `${PUBLIC_API_BASE_URL}${path}`;

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
