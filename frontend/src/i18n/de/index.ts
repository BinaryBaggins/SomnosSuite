import type { Translation } from '../i18n-types';

const de = {
	common: {
		languageLabel: 'Sprache',
		localeOptionEn: 'Englisch',
		localeOptionDe: 'Deutsch'
	},
	login: {
		title: 'Anmeldung | SomnosSuite',
		internalLabel: 'SomnosSuite Intern',
		headline: 'Operations-Portal fuer Teams im Schlafprogramm.',
		subline:
			'Verwalte Interventionen, pruefe naechstliche Trends und koordiniere Patienten-Nachverfolgung in einem sicheren Arbeitsbereich.',
		benefitSecure: 'Geschuetzte Datenfluesse und protokollierte Zugriffe',
		benefitDashboards: 'Gemeinsame Dashboards fuer Behandlungs- und Forschungsteams',
		benefitTriage: 'Schnelle Incident-Triage mit rollenbasierten Ansichten',
		envLabel: 'Staging',
		buildLabel: 'Build 0.1.0',
		signInHeading: 'Anmelden',
		signInSubline: 'Melde dich mit deinem Organisationskonto an.',
		workEmailLabel: 'Arbeits-E-Mail',
		workEmailPlaceholder: 'name@firma.com',
		passwordLabel: 'Passwort',
		passwordPlaceholder: 'Passwort eingeben',
		rememberDevice: 'Dieses Geraet merken',
		forgotPassword: 'Passwort vergessen?',
		signInButton: 'Bei SomnosSuite anmelden',
		orLabel: 'oder',
		ssoButton: 'Mit SSO fortfahren',
		needAccess: 'Zugang anfragen? Admin kontaktieren',
		systemStatus: 'Systemstatus'
	}
} satisfies Translation;

export default de;
