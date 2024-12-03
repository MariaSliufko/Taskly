import { Transition } from '@headlessui/react';

type LoadingProps = {
  transitionBetweenState?: boolean;
  loading?: boolean;
  customPlacement?: boolean;
};
export default function Loading({ transitionBetweenState = false, loading = true, customPlacement }: LoadingProps) {
  return transitionBetweenState ? (
    <Transition
      data-testid='loading'
      as='div'
      appear={true}
      show={!loading}
      enter='transition-opacity duration-0'
      enterTo='opacity-100'
      leave='transition-opacity duration-1000'
      leaveFrom='opacity-100'
      leaveTo='opacity-0'>
      <div className='relative'>
        <div className={`${!customPlacement && 'absolute left-1/2 -translate-x-1/2 transform'} `}>
          <svg className='text-white -ml-1 mr-3 h-5 w-5 animate-spin' xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 24 24'>
            <circle className='opacity-25' cx='12' cy='12' r='10' stroke='currentColor' strokeWidth='4'></circle>
            <path
              className='opacity-75'
              fill='currentColor'
              d='M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z'></path>
          </svg>
        </div>
      </div>
    </Transition>
  ) : (
    <div className={`${!loading && 'hidden'} ${!customPlacement && 'fixed inset-0 top-4'} flex justify-center`} data-testid='loading'>
      <svg className='text-white -ml-1 mr-3 h-5 w-5 animate-spin' xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 24 24'>
        <circle className='opacity-25' cx='12' cy='12' r='10' stroke='currentColor' strokeWidth='4'></circle>
        <path
          className='opacity-75'
          fill='currentColor'
          d='M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z'></path>
      </svg>
    </div>
  );
}
