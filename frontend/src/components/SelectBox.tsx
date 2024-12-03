import { Combobox, ComboboxButton, ComboboxInput, ComboboxOption, ComboboxOptions, Field, Label } from '@headlessui/react';
import { CheckIcon, ChevronDownIcon, XMarkIcon } from '@heroicons/react/16/solid';
import { useEffect, useState } from 'react';

export type SelectBoxProps = {
  label?: string;
  options: OptionsProps[];
  defaultSelectedOption: OptionsProps[] | null;
  multiple?: boolean;
  disabled?: boolean;
  allowCreate?: boolean;
  placeholder?: string;
  invalid?: boolean;
  onChange: (selectedOption: OptionsProps[] | null) => void;
};

export type OptionsProps = {
  value: string;
  label: string;
};

export default function SelectBox({
  label,
  options,
  defaultSelectedOption,
  multiple = false,
  disabled = false,
  allowCreate = false,
  placeholder = 'Select...',
  invalid = false,
  onChange,
}: SelectBoxProps) {
  const [query, setQuery] = useState<string>('');
  const [selectOptions, setSelectOptions] = useState<OptionsProps[]>(options);
  const [selectedOption, setSelectedOption] = useState<OptionsProps[] | null>(defaultSelectedOption);

  const searchMatchingOptions = (options: OptionsProps[], query: string | null) => {
    if (!query) {
      return options;
    }
    return options.filter(option => option.label.toLowerCase().includes(query.toLowerCase())) ?? [];
  };

  useEffect(() => {
    if (defaultSelectedOption !== selectedOption) {
      onChange(selectedOption);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [selectedOption]);

  const handleKeyDown = (event: { key: string; currentTarget: { value: any } }) => {
    if (allowCreate && event.key === 'Enter' && searchMatchingOptions(options, query).length === 0) {
      const newOption = { value: event.currentTarget.value, label: event.currentTarget.value };
      setSelectOptions([...selectOptions, newOption]);
      setTimeout(() => {
        setSelectedOption([newOption]);
      }, 10);
    }
  };
  const maxWidthOfOptions = selectOptions.reduce((max, option) => {
    return Math.max(max, option.label.length);
  }, 0);

  return (
    <Field>
      {label && <Label className="cursor-pointer text-primary text-sm block font-['Roboto']">{label}</Label>}
      <Combobox
        immediate
        disabled={disabled}
        multiple={multiple}
        value={selectedOption ? selectedOption.map(option => option.value) : []}
        onClose={() => setQuery('')}
        onChange={value => {
          if (value && selectOptions) {
            const selectedOptions = Array.isArray(value)
              ? selectOptions.filter(option => value.some(val => val === option.value))
              : selectOptions.filter(option => value === option.value);
            setSelectedOption(selectedOptions);
            setQuery('');
          } else {
            setSelectedOption(null);
          }
        }}>
        {multiple && selectedOption && selectedOption.length > 0 && (
          <ul className='flex flex-wrap gap-2 my-2 w-2/3 '>
            {selectedOption.map((option: OptionsProps) => (
              <li className='flex items-center' key={option.value}>
                <div className='flex gap-2 px-2 py-1 rounded bg-secondary text-secondary items-center'>
                  <div>{option.label}</div>
                  <div>
                    <XMarkIcon
                      className='size-5 fill-primary cursor-pointer'
                      onClick={() => {
                        setSelectedOption(selectedOption.filter(selected => selected.value !== option.value));
                      }}
                    />
                  </div>
                </div>
              </li>
            ))}
          </ul>
        )}
        <div className={`flex items-center ${disabled ? 'opacity-60' : ''}`}>
          <ComboboxInput
            spellCheck={false}
            className={`cursor-default text-primary outline-none p-1 mt-2 mb-4 rounded-sm bg-muted  select-none ${maxWidthOfOptions > 3 ? 'w-fit' : 'w-12'} ${invalid ? 'ring-2 ring-danger' : ''}`}
            displayValue={(item: string[]) => {
              if (multiple) return '';
              if (!item || item.length === 0) return '';
              return (
                selectOptions.find(option => {
                  return option.value === item[0];
                })?.label ?? ''
              );
            }}
            onKeyDown={handleKeyDown}
            onChange={event => {
              event.preventDefault();
              setQuery(event.currentTarget.value);
            }}
            placeholder={placeholder}
          />
          <ComboboxButton className='bg-primary text-primary rounded-sm p-[2px] ml-2 -mt-2' as='div'>
            <ChevronDownIcon className={`size-5 ${disabled ? 'cursor-not-allowed' : 'cursor-pointer'}`} />
          </ComboboxButton>

          <ComboboxOptions
            anchor='bottom start'
            className='min-w-[var(--input-width)] select-none bg-gray-200 shadow-sm shadow-highlight mt-1
              empty:hidden'>
            {allowCreate && query?.length > 0 && searchMatchingOptions(selectOptions, query).length === 0 ? (
              <ComboboxOption value={query} key={query} className='data-[focus]:bg-danger text-primary'>
                <div className='flex flex-col gap-1 items-center pl-1 py-2'>
                  <div className='text-sm font-semibold'>"{query}"</div>
                  <div className='text-xs'>Does not exist (enter to create)</div>
                </div>
              </ComboboxOption>
            ) : (
              searchMatchingOptions(selectOptions, query).map((option, x) => (
                <ComboboxOption
                  key={option.value}
                  value={option.value ?? ''}
                  className='group flex gap-1 items-center p-2 select-none data-[focus]:bg-primary data-[focus]:text-primary cursor-pointer'>
                  {multiple && <CheckIcon className='invisible size-3 group-data-[selected]:visible' />}
                  {option.label}
                </ComboboxOption>
              ))
            )}
          </ComboboxOptions>
        </div>
      </Combobox>
    </Field>
  );
}
