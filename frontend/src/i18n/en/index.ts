import type { BaseTranslation } from '../i18n-types'

const en = {
	common: {
		languageLabel: 'Language',
		localeOptionEn: 'English',
		localeOptionDe: 'Deutsch'
	},
	login: {
		title: 'Login | SomnosSuite',
		internalLabel: 'SomnosSuite Internal',
		headline: 'Operations portal for sleep program teams.',
		subline:
			'Manage interventions, review nightly trends, and coordinate patient follow-ups in one secure workspace.',
		benefitSecure: 'Protected data flows and audited access',
		benefitDashboards: 'Shared dashboards for care and research teams',
		benefitTriage: 'Fast incident triage with role-based views',
		envLabel: 'Staging',
		buildLabel: 'Build 0.1.0',
		signInHeading: 'Sign in',
		signInSubline: 'Use your organization account to continue.',
		workEmailLabel: 'Work email',
		workEmailPlaceholder: 'name@company.com',
		passwordLabel: 'Password',
		passwordPlaceholder: 'Enter your password',
		rememberDevice: 'Remember this device',
		forgotPassword: 'Forgot password?',
		signInButton: 'Sign in to SomnosSuite',
		orLabel: 'or',
		ssoButton: 'Continue with SSO',
		needAccess: 'Need access? Contact admin',
		systemStatus: 'System status'
	}
} satisfies BaseTranslation

export default en
