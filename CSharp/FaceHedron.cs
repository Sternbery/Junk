using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace polyhedraV3{
	public class FaceHedron <F,E,V>
	{
		private Face[] faces;
		private F[] faceData;
		private E[] edgeData;
		private V[] vertexData;
	
		public FaceHedron(Face[] fs, F[] fds, E[] eds, V[] vds){
			faces = fs;
			faceData = fds;
			edgeData = eds;
			vertexData=vds;
		}
		public FaceHedron(List<Face> fs, List<F> fds, List<E> eds, List<V> vds){
			faces = fs.ToArray();
			faceData = fds.ToArray();
			edgeData = eds.ToArray();
			vertexData=vds.ToArray();
		}
		public FaceHedron(EdgeHedron<F,E,V> eh){
			
			List<Edge> seenedges = new List<Edge>();
			List<Face> faces = new List<Face>();
			
			foreach(Edge edge in eh.Edges){
				if(seenedges.Contains(edge)) continue;
				
				Face face = new Face();
				int currIndex = edge.N();
				Edge curr = edge.next(eh);
				
				while(curr != edge){
					seenedges.Add(curr);
					face.Add(curr.B(),currIndex);
					
					currIndex = curr.N();
					curr = curr.next(eh);
				}
				face.Add(curr.B(),currIndex);
				
				faces.Add(face);
			}
			
			this.faces = faces.ToArray();
			this.faceData = eh.FaceData;
			this.edgeData = eh.EdgeData;
			this.vertexData=eh.VertexData;
		}
		
		public Face[] Faces{
			get{
				return faces;
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
		
		public FaceHedron<O,E,V> MapFaces<O>(Func<Face,F,O> f2o){
			return new FaceHedron<O,E,V>(
				this.faces,
				this.faces
					.Select(f=>f2o(f,f.GetData(this)))
					.ToArray(),
				this.edgeData,
				this.vertexData
			);
		}
		public FaceHedron<O,E,V> MapFaces<O>(Func<F,O> f2o){
			return new FaceHedron<O,E,V>(
				this.faces,
				this.faceData.Select(f2o).ToArray(),
				this.edgeData,
				this.vertexData
			);
		}
		public FaceHedron<F,O,V> MapEdges<O>(Func<E,O> e2o){
			return new FaceHedron<F,O,V>(
				this.faces,
				this.faceData,
				this.edgeData.Select(e2o).ToArray(),
				this.vertexData
			);
		}
		public FaceHedron<F,E,O> MapVerts<O>(Func<Face,V,O> v2o){
			return this.ToEdgeHedron().Invert().ToFaceHedron().MapFaces(v2o).ToEdgeHedron().Invert().ToFaceHedron();
			// return new FaceHedron<O,E,V>(
				// this.faces,
				// this.faceData,
				// this.edgeData,
				//this.vertexData.Select(v=>)
			// );
		}
		public FaceHedron<F,E,O> MapVerts<O>(Func<V,O> v2o){
			return new FaceHedron<F,E,O>(
				this.faces,
				this.faceData,
				this.edgeData,
				this.vertexData.Select(v2o).ToArray()
			);
		}
		
		public FaceHedron<V,E,F> Invert(){
			return this.ToEdgeHedron().Invert().ToFaceHedron();
		}
		
		public int NumFaces{
			get{
				return faces.Length;
			}
		}
		
		public EdgeHedron<F,E,V> ToEdgeHedron(){
			return new EdgeHedron<F,E,V>(this);
		} 
	}
}
