﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_flashcards_dotnet.Data;
using api_flashcards_dotnet.Dtos;
using api_flashcards_dotnet.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_flashcards_dotnet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecksController : Controller
    {
        private readonly FlashcardDataRepository _flashcardDataRepository;
        private readonly IMapper _mapper;

        public DecksController(FlashcardDataRepository flashcardDataRepository, IMapper mapper)
        {
            _flashcardDataRepository = flashcardDataRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Deck>>> Get()
        {
            List<Deck> _decks = await _flashcardDataRepository.GetAllDecks();


            List<DeckResponse> _decksResponse =_mapper.Map<List<DeckResponse>>(_decks);

            return Ok(_decksResponse);
        }

        [HttpPost]
        public async Task<ActionResult<Deck>> AddDeck([FromBody] Deck deck)
        {
            await _flashcardDataRepository.AddDeck(deck.Name);
            return CreatedAtAction(nameof(Get), new { id = deck.Id }, deck); 
        }
    }
}
