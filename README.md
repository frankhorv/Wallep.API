# Wallet.API

.Net Core backend service exposing HTTP REST API and implementing player's wallet.

## Functional requirements:
- Player is identified by Guid identifier
- Player's wallet consists of balance (decimal value), currency is neglected, we expect everything to be in single common currency, e.g. EURO
- balance is manipulated via transactions (identified by unique Guid identifier, having transaction type and decimal money amount to add/substract to the wallet balance)
- possible transaction types:
- deposit (increments balance)
- stake (decrements balance)
- win (increments balance)
- balance may never drop below 0 (transaction has to be rejected), in other case it is accepted
- each transaction should guarantee idempotent behavior (might be called several times with the same server state):
- if rejected initially => has to be rejected also on repeats
- if accepted initially => has to return accepted on repeats (however affecting balance just once)
- single player's transactions should be serializable (concurrent transactions of the same player should be processed in any order, but should keep player's wallet in a consistent state)

## Design APIs (contracts) for following operations:
- register wallet for new player with initial balance 0 (should return error if player's wallet is already registered)
- get player's balance
- credit transaction to player's wallet (returns accepted/rejected)

## Nonfunctional requirements:
- service might be single noded
- you don't need to implement data layer with real persistence, just define needed repository interfaces and provide its mocked in-memory implementions
