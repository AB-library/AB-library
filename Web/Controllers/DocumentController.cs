using BusinessLogic.Services;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABLibraryAPI.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentController : ControllerBase {
    private readonly DocumentService _documentService;

    public DocumentController(DocumentService documentService) {
        _documentService = documentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DocumentDTO>>> GetAll() {
        var documents = await _documentService.GetAll();
        return Ok(documents);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<DocumentDTO>> GetById(string id) {
        var document = await _documentService.GetById(id);
        if (document == null)
            return NotFound();
        return Ok(document);
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(DocumentDTO documentDto) {
         var created = await _documentService.Create(documentDto);
        return CreatedAtAction(nameof(GetById), new { id = created?.Id }, created);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, DocumentDTO documentDto) {
        var updated = await _documentService.Update(id, documentDto);
        if (!updated)
            return NotFound();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id) {
        var deleted = await _documentService.Delete(id);
        if (!deleted)
            return NotFound();
        return NoContent();
    }
}