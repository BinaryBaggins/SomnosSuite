import type { BaseTranslation } from '../i18n-types';

const en = {
	common: {
		languageLabel: 'Language',
		localeOptionEn: 'English',
		localeOptionDe: 'Deutsch'
	},
	login: {
		title: 'Login | SomnosSuite',
		internalLabel: 'SomnosSuite',
		headline: 'Control portal for livestock stunning workflows.',
		subline:
			'Record animals, stunning devices, stunning checks, outcomes, and audit metadata in one secure documentation workspace.',
		benefitSecure: 'Tamper-resistant records with audited user actions',
		benefitDashboards: 'Operational dashboards for line teams and compliance reporting',
		benefitTriage: 'Fast exception handling with role-based views',
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
	},
	dashboard: {
		title: 'Dashboard | SomnosSuite',
		headline: 'Welcome to the stunning control and documentation dashboard'
	}
} satisfies BaseTranslation;

export default en;
