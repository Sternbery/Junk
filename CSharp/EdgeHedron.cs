using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace polyhedraV3{
	public class EdgeHedron <F,E,V>
	{
		private Edge[] edges;
		private E[] edgeData;
		private F[] faceData;
		private V[] vertexData;
		
		public E getE(int i){
			return edgeData[i];
		}
		public F getF(int i){
			return faceData[i];
		}
		public V getV(int i){
			return vertexData[i];
		}
		public Edge getEdge(int i){
			return edges[i];
		}
		
		public Edge[] Edges{
			get{
				return edges;
			}
		}
		public F[] FaceData{
			get{
				return faceData;
			}
		}
		public E[] EdgeData{
			get{
				return edgeData;
			}
		}
		public V[] VertexData{
			get{
				return vertexData;
			}
		}
		
		
		public EdgeHedron(Edge[] es, F[] fds, E[] eds, V[] vds){
			this.edges = es;
			this.faceData = fds;
			this.edgeData = eds;
			this.vertexData = vds;
		}
		public EdgeHedron(List<Edge> es, List<F> fds, List<E> eds,  List<V> vds){
			edges = es.ToArray();
			faceData = fds.ToArray();
			vertexData = vds.ToArray();
			edgeData = eds.ToArray();
		}
		
		public EdgeHedron<V,E,F> Invert(){
			return new EdgeHedron<V,E,F>(
					this.edges
						.Select(e=>e.invert(this))
						.ToArray(),
					this.vertexData,
					this.edgeData,
					this.faceData
				);
		}
		
		public static void matchPartials(List<Edge> mypes){
			List<Edge> copy = new List<Edge>(mypes.Count);
			copy.AddRange(mypes);
			matchPartials(copy,mypes);
		}
		public static void matchPartials(List<Edge> mypes, List<Edge> original){
			if(mypes.Count==0)
				return;
			
			while(mypes.Count!=0){
				
				bool foundMatch = false;
				
				Edge e1 = mypes[0];
				for(int i=1; i<mypes.Count; i++){
					Edge e2 = mypes[i];
					if(e1.f == e2.b && e1.b == e2.f){
						e1.r = e2.l;
						e2.r = e1.l;
						e1.rev = original.IndexOf(e2);
						e2.rev = original.IndexOf(e1);

						mypes.RemoveAt(i);
						mypes.RemoveAt(0);
						
						foundMatch = true;
						break;
					}
				}
				
				if(!foundMatch)
					throw new ArgumentException("Missing match. Not all given edges have a match");
			}
		}
	}
}