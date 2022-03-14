Quest Machine - NPC Quester Example

This example demonstrates multiple NPCs completing quests.

The questers are two knights (Knight1 & Knight2).

The quest giver is a job-posting sign.

There are two quests: Get 3 Gold and Kill 2 Orcs.

This example uses the following free assets:

- Cinemachine
- A Piece of Nature - Evgenia: https://assetstore.unity.com/packages/3d/environments/fantasy/a-piece-of-nature-40538
- Toon RTS Units Demo - Polygon Blacksmith: https://assetstore.unity.com/packages/3d/characters/toon-rts-units-demo-69687
- Toon RTS Unit Orcs Demo - https://assetstore.unity.com/packages/3d/characters/humanoids/toon-rts-units-orcs-demo-101359


Notes:
- Each quester (Knight1 & Knight2) has a Quest Journal with a unique ID, and
  a separate quest HUD.
- The quests use counters that listen for a message such as "Got Gold" sent
  from the quester. So if Knight2 has the Get 3 Gold quest, only Gold Gold
  messages sent from Knight2 will increment the counter.
- The blue knight is the Quest Giver.
