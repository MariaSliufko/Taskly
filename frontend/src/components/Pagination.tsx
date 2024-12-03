import SelectBox from '../components/SelectBox'
import Button from '../components/Button'
import { ChevronLeftIcon, ChevronRightIcon } from '@heroicons/react/16/solid';
import { useEffect, useState } from 'react';

type PaginationProps = {
  totalSize: number;
  pageSize: PageSize;
  currentPage: number;
  onPageChange: (page: number) => void;
  onPageSizeChange?: (pageSize: PageSize) => void;
};

export type PageSize = 10 | 20 | 50 | 100;

export default function Pagination({ totalSize, pageSize, currentPage, onPageChange, onPageSizeChange }: PaginationProps) {

  const [totalPages, setTotalPages] = useState(Math.ceil(totalSize / pageSize));
  useEffect(() => {
    setTotalPages(Math.ceil(totalSize / pageSize));
  }, [totalSize, pageSize, currentPage]);
  const options: { value: PageSize; label: number }[] = [
    {
      value: 10,
      label: 10,
    },
    {
      value: 20,
      label: 20,
    },
    {
      value: 50,
      label: 50,
    },
    {
      value: 100,
      label: 100,
    },
  ];

  return (
    <div className='flex items-center gap-3'>
      <div className='-mt-2'>'Items Per Page'</div>
      <div>
        <SelectBox
          placeholder=''
          options={options.map(option => ({ value: option.value.toString(), label: option.label.toString() }))}
          defaultSelectedOption={[{ value: pageSize.toString(), label: pageSize.toString() }]}
          disabled={totalPages === 0}
          onChange={selection => {
            if (selection && onPageSizeChange) {
              onPageSizeChange(Number(selection[0].value) as PageSize);
            }
          }}
        />
      </div>
      <div className='-mt-2  flex items-center gap-1 bg-muted rounded-md py-1 px-4'>
        <div>'Page'</div>
        <Button buttonStyle='link' onClick={() => onPageChange(currentPage - 1)} disabled={totalPages === 0 || currentPage === 1} title='PreviousPage'>
          <ChevronLeftIcon className='size-4' />
        </Button>
        <div>
          {currentPage} 'Of' {totalPages > 0 ? totalPages : 1}
        </div>
        <Button
          buttonStyle='link'
          onClick={() => onPageChange(currentPage + 1)}
          disabled={totalPages === 0 || currentPage === totalPages}
          title='NextPage'>
          <ChevronRightIcon className='size-4' />
        </Button>
      </div>
      <div className='-mt-2'></div>
    </div>
  );
}
