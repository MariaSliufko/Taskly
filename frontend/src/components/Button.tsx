import { Button as HeadlessButton } from '@headlessui/react';
export type ButtonProps = {
  onClick?: () => void;
  children?: React.ReactNode;
  disabled?: boolean;
  buttonStyle: ButtonStyleProps;
  className?: string;
  title?: string;
};
export type ButtonStyleProps = 'primary' | 'primary-outlined' | 'secondary' | 'secondary-outlined' | 'danger' | 'link';

export default function Button({ onClick, children, disabled, buttonStyle, className, title }: ButtonProps): JSX.Element {
  const getClasses = (buttonStyle: ButtonStyleProps) => {
    switch (true) {
      case 'primary' === buttonStyle:
        return 'bg-primary text-primary px-4 py-2';
      case 'secondary' === buttonStyle:
        return 'bg-secondary text-secondary px-4 py-2';
      case 'primary-outlined' === buttonStyle:
        return 'border border-primary text-primary px-4 py-2';
      case 'secondary-outlined' === buttonStyle:
        return 'border border-secondary text-primary px-4 py-2';
      case 'danger' === buttonStyle:
        return 'bg-danger text-primary px-4 py-2';
      case 'link' === buttonStyle:
        return 'bg-none text-primary';
      default:
        return '';
    }
  };

  return (
    <HeadlessButton
      {...{ title: title }}
      onClick={onClick}
      disabled={disabled}
      className={`w-fit rounded-md font-['Roboto'] select-none ${getClasses(buttonStyle)} disabled:opacity-60 disabled:cursor-not-allowed ${className ?? ''}`}>
      {children}
    </HeadlessButton>
  );
}
