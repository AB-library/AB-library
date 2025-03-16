using System.Reflection.Metadata;
using BusinessLogic.Abstractions;
using DataAccess.Models;
using DataAccess.Repositories;

namespace BusinessLogic.Services;

public class DocumentService {
    private DocumentRepository _repository;

    public DocumentService(DocumentRepository repository) {
        _repository = repository;
    }

    public async Task<List<DocumentDTO>> GetAll() {
        var documents = await _repository.GetAllAsync();
        return documents.Select(d => new DocumentDTO {
            Id = d.Id,
            Title = d.Title,
            Content = d.Content,
            Categories = d.Categories,
            CreatedOn = d.CreatedOn
        }).ToList();
    }

    public async Task<DocumentDTO?> GetById(string id) {
        var document = await _repository.GetByIdAsync(id);
        return document == null ? null : new DocumentDTO {
            Id = document.Id,
            Title = document.Title,
            Content = document.Content,
            Categories = document.Categories,
            CreatedOn = document.CreatedOn
        };
    }

    public async Task<DocumentDTO?> Create(DocumentDTO documentDto) {
        DocumentEntity document = new() {
            Title = documentDto.Title,
            Content = documentDto.Content,
            Categories = documentDto.Categories,
            CreatedOn = DateTime.UtcNow
        };

        var createdDocument = await _repository.CreateAsync(document);
        if (createdDocument == null)
            return null;

        return new DocumentDTO {
            Id = createdDocument.Id,
            Title = createdDocument.Title,
            Content = createdDocument.Content,
            Categories = createdDocument.Categories,
            CreatedOn = createdDocument.CreatedOn
        };
    }

    public async Task<bool> Update(string id, DocumentDTO documentDto) {
        var existingDocument = await _repository.GetByIdAsync(id);
        if (existingDocument == null)
            return false;

        existingDocument.Title = documentDto.Title;
        existingDocument.Content = documentDto.Content;
        existingDocument.Categories = documentDto.Categories;
        existingDocument.CreatedOn = documentDto.CreatedOn;
        
        await _repository.UpdateAsync(existingDocument);
        return true;
    }

    public async Task<bool> Delete(string id) {
        var existingDocument = await _repository.GetByIdAsync(id);
        if (existingDocument == null)
            return false;

        await _repository.DeleteByIdAsync(id);
        return true;
    }
}