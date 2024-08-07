﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace SatisfactoryPlanner.Modules.Resources.Domain.Nodes
{
    public interface INodeRepository
    {
        /// <summary>
        ///     Find the <see cref="Node" /> with the given <paramref name="nodeId" />, or null if not found.
        /// </summary>
        Node? FindById(NodeId nodeId);

        /// <summary>
        ///     Get all the nodes.
        /// </summary>
        Task<List<Node>> GetAllAsync();

        /// <summary>
        ///     Get the <see cref="Node" /> with the given <paramref name="nodeId" />.
        ///     If no node is found, an exception is thrown.
        /// </summary>
        Task<Node> GetByIdAsync(NodeId nodeId);
    }
}