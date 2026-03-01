import type { Mechanic } from "~/types/mechanic";

const mechanics: Mechanic[] = [
  {
    id: 1,
    name: "Петрович"
  },
  {
    id: 2,
    name: "Иванович"
  }
]

export default eventHandler(async () => {
  return mechanics
})
