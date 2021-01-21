#José Luís Costa 92996
#Diogo Mendonça 93002
#Guilherme Pereira 93134

import asyncio
import getpass
import json
import os
import random
from my_new_game import *
from copy import deepcopy
from tree_searchDeadlock2 import SearchTreeD2
from tree_searchAS import SearchTreeBoxAS
from tree_searchKpath import SearchTreeKpath
from my_new_mapa import Map
from consts import *
import time
import sys
import asyncio
import getpass
import json
import os
import websockets
import requests
import re

def deathSquares2(level):
    startD = Deadlock(level)
    floorTiles = startD.map.filter_tiles([Tiles.FLOOR])
    group = {}
    for tile in floorTiles:
        startD.resetBoard()
        startD.setBox(tile)
        search1 = SearchTreeD2(startD)
        group[tile] = search1.search()
    teste = {}
    for i in group:
        list11 = []
        for item in group[i]:
            list11.append(item['box'])
        if list11 == []:
            teste['vazio'] = { 'max':0,'cur':0,'floorTiles' : set() }
        else:
            teste[str(list11)] = { 'max':len(list11),'cur':0,'floorTiles' : set() }
    
    for i in group:
        list11 = []
        for item in group[i]:
            list11.append(item['box'])
        if list11 == []:
            teste['vazio']['floorTiles'].add(i)
        else:
            teste[str(list11)]['floorTiles'].add(i)
    return teste


async def solver(puzzle, solution):
    totalTime = 0
    while True:
        tick = time.time()
        game_properties = await puzzle.get()
        search = SearchTreeBoxAS(BoxGame(game_properties["map"],deathSquares2(game_properties["map"])))
        nodes = await search.search()
        niv = re.findall(r'[0-9]+',game_properties["map"])[0]
        allPos = [a.move for a in nodes][1:]
        keys = []
        map1 = Map(game_properties["map"])
        oldKeeper = map1.keeper
        oldBoxes = map1.filter_tiles([Tiles.BOX,Tiles.BOX_ON_GOAL])
        for pos in allPos:
            searchK = SearchTreeKpath(map1,oldKeeper,oldBoxes,pos[0])
            res = await searchK.search()
            keys.extend([a.move for a in res][1:] + [a for a in pos[1]])
            oldKeeper = pos[2]
            oldBoxes.remove(pos[4])
            oldBoxes.append(pos[3])
        await solution.put(keys)
        totalTime += time.time() - tick
        #print("keys->" + str(keys))
        print(f"Nivel -> {niv} ,tempo -> {time.time() - tick}, tempoT -> {totalTime}")
        
async def agent_loop(puzzle, solution, server_address="localhost:8000", agent_name="ez"):
    async with websockets.connect(f"ws://{server_address}/player") as websocket:

        # Receive information about static game properties
        await websocket.send(json.dumps({"cmd": "join", "name": agent_name}))

        while True:
            try:
                update = json.loads(
                    await websocket.recv()
                )  # receive game update, this must be called timely or your game will get out of sync with the server

                if "map" in update:
                    # we got a new level
                    game_properties = update
                    keys = ""
                    await puzzle.put(game_properties)

                if not solution.empty():
                    keys = await solution.get()

                key = ""
                if len(keys):  # we got a solution!
                    key = keys[0]
                    keys = keys[1:]

                await websocket.send(
                    json.dumps({"cmd": "key", "key": key})
                )

            except websockets.exceptions.ConnectionClosedOK:
                print("Server has cleanly disconnected us")
                sys.exit(0)
                return

# DO NOT CHANGE THE LINES BELLOW
# You can change the default values using the command line, example:
# $ NAME='arrumador' python3 client.py
loop = asyncio.get_event_loop()
SERVER = os.environ.get("SERVER", "localhost")
PORT = os.environ.get("PORT", "8000")
NAME = "92996_93134_93002_"

puzzle = asyncio.Queue(loop=loop)
solution = asyncio.Queue(loop=loop)

net_task = loop.create_task(agent_loop(puzzle, solution, f"{SERVER}:{PORT}", NAME))
solver_task = loop.create_task(solver(puzzle, solution))

loop.run_until_complete(asyncio.gather(net_task, solver_task))
loop.close()
