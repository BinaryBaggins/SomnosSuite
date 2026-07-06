import type { Translation } from '../i18n-types';

const de = {
	common: {
		languageLabel: 'Sprache',
		localeOptionEn: 'Englisch',
		localeOptionDe: 'Deutsch'
	},
	login: {
		title: 'Anmeldung | SomnosSuite',
		internalLabel: 'SomnosSuite',
		headline: 'Kontrollportal fuer Nutzier-Betäubungsabläufe.',
		subline:
			'Erfasse Tiere, Betäubungsgeräte, Betäubungsprüfungen, Ergebnisse und Audit-Metadaten in einem sicheren Dokumentationsbereich.',
		benefitSecure: 'Manipulationssichere Datensätze mit protokollierten Benutzeraktionen',
		benefitDashboards: 'Operative Dashboards für Linien-Teams und Compliance-Reporting',
		benefitTriage: 'Schnelle Ausnahmebearbeitung mit rollenbasierten Ansichten',
		envLabel: 'Staging',
		buildLabel: 'Build 0.1.0',
		signInHeading: 'Anmelden',
		signInSubline: 'Melde dich mit deinem Organisationskonto an.',
		workEmailLabel: 'Arbeits-E-Mail',
		workEmailPlaceholder: 'name@firma.com',
		passwordLabel: 'Passwort',
		passwordPlaceholder: 'Passwort eingeben',
		rememberDevice: 'Dieses Gerät merken',
		forgotPassword: 'Passwort vergessen?',
		signInButton: 'Bei SomnosSuite anmelden',
		orLabel: 'oder',
		ssoButton: 'Mit SSO fortfahren',
		needAccess: 'Zugang anfragen? Admin kontaktieren',
		systemStatus: 'Systemstatus'
	},
	dashboard: {
		title: 'Dashboard | SomnosSuite',
		headline: 'Willkommen im Dashboard für Betäubungskontrolle und Dokumentation'
	}
} satisfies Translation;

export default de;
