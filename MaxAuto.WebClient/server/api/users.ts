import type {User} from '~/types/user'

const users: User[] = [
  {
    id: 1,
    firstName: 'Луцко',
    lastName: 'Михаил',
    email: 'lutsko86@gmail.com',
    role: "admin",
    position: "Программист"
  },
  {
    id: 2,
    firstName: 'Анна',
    lastName: 'Анна',
    email: 'bg.example.com',
    role: "manager",
    position: "Адмитнистратор",
    workshop: {
      id: 1,
      name: "Max Auto",
      location: "Бургас",
      address: "ул. Приморска 123"
    }
  },
  {
    id: 3,
    firstName: 'Тая',
    lastName: 'Тая',
    email: 'sb@example.com',
    role: "manager",
    position: "Адмитнистратор",
    workshop: {
      id: 2,
      name: "Max Auto",
      location: "Солнечный берег",
      address: "ул. Чайка 45"
    }
  }
]

export default eventHandler(async () => {
  return users
})
