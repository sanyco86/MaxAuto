import type { Workshop } from "~/types/workshop";

const workshops: Workshop[] = [
  {
    id: 1,
    name: "Max Auto",
    location: "Бургас",
    address: "ул. Приморска 123"
  },
  {
    id: 2,
    name: "Apex 99",
    location: "Солнечный берег",
    address: "ул. Чайка 45"
  }
]

export default eventHandler(async () => {
  return workshops
})
